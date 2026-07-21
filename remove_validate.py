import os
import glob
import re

for file in glob.glob('AlphaX.FormulaEngine/Formulas/**/*.cs', recursive=True):
    with open(file, 'r', encoding='utf-8') as f:
        content = f.read()
    if 'ValidateArgumentCount(context.Args);' in content:
        content = re.sub(r'[ \t]*ValidateArgumentCount\(context\.Args\);\r?\n', '', content)
        with open(file, 'w', encoding='utf-8') as f:
            f.write(content)
