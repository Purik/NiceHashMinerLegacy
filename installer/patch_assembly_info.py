import io
import argparse
import logging
import sys
import os
import re

import requests
import zipfile


logging.basicConfig(format='%(levelname)s:  %(pathname)s \n\t%(message)s', level=logging.DEBUG, stream=sys.stdout)

BASE_DIR = os.path.realpath(os.path.dirname(__file__))
print(f'BASE_DIR = "{BASE_DIR}"')


parser = argparse.ArgumentParser()
parser.add_argument('--version', type=str, required=True, help='Version.')
parser.add_argument('--secret', help='Secret token for server software version update')
args = parser.parse_args()


patching_file = os.path.realpath(os.path.join(BASE_DIR, '../NiceHashMiner/WebAPI.cs', ))
with open(patching_file, 'r+') as f:
    content = f.read()
    find = 'private static string SOFTWARE_VERSION(.)*;'
    replace = f'private static string SOFTWARE_VERSION = "{args.version}";'
    content = re.sub(find, replace, content)
    f.seek(0)
    f.truncate(0)
    f.write(content)


URL = 'https://freezone.name/ajax/nicehash-software-version'
assembly_file = os.path.realpath(os.path.join(BASE_DIR, '../NiceHashMiner/Properties/AssemblyInfo.cs', ))
orig_version = None
with open(assembly_file, 'r+') as f:
    content = f.read()

    search = re.search('AssemblyVersion(\(")\d.\d.\d.\d"\)', content).group(0)
    orig_version = re.search('\d.\d.\d.\d', search).group(0)
    resp = requests.post(URL, dict(version=orig_version, token=args.secret))

    find = ''

    # find = 'AssemblyVersion(.)*'
    # replace = f'AssemblyVersion("{args.version}")]'
    # content = re.sub(find, replace, content)

    # find = 'AssemblyFileVersion(.)*'
    # replace = f'AssemblyFileVersion("{args.version}")]'
    # content = re.sub(find, replace, content)

    content = re.sub('AssemblyProduct\("NiceHashMinerLegacy"\)', f'AssemblyProduct("CryptoMiner v {args.version}")', content)
    content = re.sub('NiceHashMinerLegacy', 'CryptoMiner', content)
    content = re.sub('NiceHash', 'CryptoMiner', content)

    f.seek(0)
    f.truncate(0)
    f.write(content)

if orig_version is None:
    raise ValueError(f'orig_version is empty')

# Download dll s
print('HTTP_PROXY = ' + os.environ.get('HTTP_PROXY', ''))
print('HTTPS_PROXY = ' + os.environ.get('HTTPS_PROXY', ''))

URL = f'https://github.com/nicehash/NiceHashMinerLegacy/releases/download/{orig_version}/NHML-{orig_version}.zip'
r = requests.get(URL)
z = zipfile.ZipFile(io.BytesIO(r.content))
extract_path = os.path.join(BASE_DIR, '..', 'NiceHashOriginal')
z.extractall(extract_path)
