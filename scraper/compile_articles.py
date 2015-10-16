import shutil, glob

outfilename = "Wikipedia.ency"

with open(outfilename, 'wb') as outfile:
    for filename in glob.glob('./articles/*.txt'):
        if filename == outfilename:
            # don't want to copy the output into the output
            continue
        with open(filename, 'rb') as readfile:
            shutil.copyfileobj(readfile, outfile)