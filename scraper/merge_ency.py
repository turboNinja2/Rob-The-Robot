import shutil, glob

outfilename = "CK12_K12.ency"

with open(outfilename, 'wb') as outfile:
    for filename in glob.glob('./CK12_K12/*.ency'):
        if filename == outfilename:
            # don't want to copy the output into the output
            continue
        with open(filename, 'rb') as readfile:
            shutil.copyfileobj(readfile, outfile)