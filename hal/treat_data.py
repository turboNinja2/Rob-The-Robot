from csv import DictReader

        
def prepare_label(proposedAnswer,row):
    return row['question'] + ' ' + row['answer'+proposedAnswer]

def prepare_question(proposedAnswer,row):
    return str(int(proposedAnswer==row['correctAnswer']))
    
    
path = 'C:\\Users\\JUJulien\\Desktop\\KAGGLE\\Competitions\\Rob-The-Robot\\data\\training_set.tsv'

labels_data_path = ".\\labels.csv"
with open(labels_data_path, 'w') as outfile:
    for t, row in enumerate(DictReader(open(path), delimiter='\t')):
        print(row)
        outfile.write(prepare_label('A',row) + '\n')
        outfile.write(prepare_label('B',row) + '\n')
        outfile.write(prepare_label('C',row) + '\n')
        outfile.write(prepare_label('D',row) + '\n')


cleaned_data_path = ".\\questions.csv"
with open(cleaned_data_path, 'w') as outfile:
    for t, row in enumerate(DictReader(open(path), delimiter='\t')):
        print(row)
        outfile.write(prepare_question('A',row) + '\n')
        outfile.write(prepare_question('B',row) + '\n')
        outfile.write(prepare_question('C',row) + '\n')
        outfile.write(prepare_question('D',row) + '\n')

