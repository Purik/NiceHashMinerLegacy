import argparse
import logging
import sys
import os
import urllib.request
import pathlib
import re
import zipfile


logging.basicConfig(format='%(levelname)s:  %(pathname)s \n\t%(message)s', level=logging.DEBUG, stream=sys.stdout)

BASE_DIR = os.path.dirname(__file__)

parser = argparse.ArgumentParser()
parser.add_argument('--config', type=str, required=True, help='Configuration name.')
args = parser.parse_args()


cs_file_path = os.path.join(BASE_DIR, 'NiceHashMiner', 'Utils', 'MinersDownloadManager.cs')
with open(cs_file_path) as f:
    cs_file_lines = f.readlines()

urls = []
for line in cs_file_lines:
    try:
        url = re.search("(?P<url>https?://[^\s]+)", line).group("url")
        url = url.replace('"', '')
        url = url.replace(',', '')
        print(f'found URL: {url}')
        urls.append(url)
    except:
        pass

assert len(urls) == 2
for url in urls:
    ext = pathlib.Path(url).suffix
    assert ext == '.zip'

for url in urls:
    file_name = pathlib.Path(url).name
    dst_file = os.path.join(args.config, file_name)
    urllib.request.urlretrieve(url, dst_file)
    try:
        zip_ref = zipfile.ZipFile(dst_file, 'r')
        zip_ref.extractall(args.config)
        zip_ref.close()
    finally:
        os.remove(dst_file)
    pass
