"""Build the standalone release without changing the Unity learning project."""

import gzip
import hashlib
import io
from pathlib import Path
import re
import tarfile
import uuid
import zipfile


ROOT = Path(__file__).resolve().parents[2]
VERSION = "1.0.0"
OUTPUT = ROOT / "ReleaseArtifacts" / VERSION
SOURCE = ROOT / "Assets/Scripts/Lesson08/SingleScriptableObject.cs"
SOURCE_META = SOURCE.with_suffix(".cs.meta")
GUID = re.search(r"^guid: (\w+)$", SOURCE_META.read_text(), re.M).group(1)
IMPORT_ROOT = "Assets/SingleScriptableObject"


def stable_guid(path):
    return uuid.uuid5(uuid.NAMESPACE_URL, "Unity-ScriptableObject-Learn/" + path).hex


def file_entry(path, content, metadata=None):
    guid = stable_guid(path) if metadata is None else GUID
    if metadata is None:
        metadata = (
            f"fileFormatVersion: 2\nguid: {guid}\n"
            "TextScriptImporter:\n  externalObjects: {}\n  userData: \n"
            "  assetBundleName: \n  assetBundleVariant: \n"
        ).encode()
    return path, guid, content, metadata


def add_tar(archive, name, content):
    info = tarfile.TarInfo(name)
    info.size = len(content)
    info.mode = 0o644
    info.mtime = 0
    archive.addfile(info, io.BytesIO(content))


def main():
    OUTPUT.mkdir(parents=True, exist_ok=True)
    entries = [
        file_entry(
            IMPORT_ROOT + "/SingleScriptableObject.cs",
            SOURCE.read_bytes(), SOURCE_META.read_bytes(),
        ),
        file_entry(IMPORT_ROOT + "/README.md", (ROOT / "Docs/SingleScriptableObject.md").read_bytes()),
    ]
    package = OUTPUT / f"SingleScriptableObject-{VERSION}.unitypackage"
    with package.open("wb") as raw:
        with gzip.GzipFile(filename="", fileobj=raw, mode="wb", mtime=0) as compressed:
            with tarfile.open(fileobj=compressed, mode="w") as archive:
                for path, guid, content, metadata in entries:
                    add_tar(archive, guid + "/asset", content)
                    add_tar(archive, guid + "/asset.meta", metadata)
                    add_tar(archive, guid + "/pathname", path.encode())

    source_zip = OUTPUT / f"SingleScriptableObject-{VERSION}.zip"
    with zipfile.ZipFile(source_zip, "w", compression=zipfile.ZIP_DEFLATED) as archive:
        for path, _, content, metadata in entries:
            for name, data in ((path, content), (path + ".meta", metadata)):
                info = zipfile.ZipInfo(name, date_time=(2026, 10, 25, 0, 0, 0))
                info.compress_type = zipfile.ZIP_DEFLATED
                archive.writestr(info, data)

    # Verify both deliverables contain exactly the reviewed source and metadata.
    with tarfile.open(package, "r:gz") as archive:
        expected = {guid + "/" + leaf for _, guid, _, _ in entries for leaf in ("asset", "asset.meta", "pathname")}
        assert set(archive.getnames()) == expected
        for path, guid, content, metadata in entries:
            assert archive.extractfile(guid + "/asset").read() == content
            assert archive.extractfile(guid + "/asset.meta").read() == metadata
            assert archive.extractfile(guid + "/pathname").read().decode() == path
    with zipfile.ZipFile(source_zip) as archive:
        assert archive.testzip() is None
        assert archive.read(IMPORT_ROOT + "/SingleScriptableObject.cs") == SOURCE.read_bytes()
        assert archive.read(IMPORT_ROOT + "/SingleScriptableObject.cs.meta") == SOURCE_META.read_bytes()

    checksums = []
    artifacts = [package, source_zip]
    report = OUTPUT / "verification-result.json"
    if report.exists():
        artifacts.append(report)
    for path in artifacts:
        checksum = hashlib.sha256(path.read_bytes()).hexdigest()
        checksums.append(f"{checksum}  {path.name}")
        print(f"{path.name}: {path.stat().st_size} bytes; SHA256 {checksum}")
    (OUTPUT / "SHA256SUMS.txt").write_text("\n".join(checksums) + "\n", encoding="utf-8")
    print("Archive contents and source byte identity verified.")


if __name__ == "__main__":
    main()
