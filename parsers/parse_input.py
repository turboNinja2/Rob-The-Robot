


from parser import parser



file_name = "../data/training_set.tsv"

spam_reader = parser(file_name,delimiter=chr(9))


spam_reader.next()



for row in spam_reader:
  print row
  break
