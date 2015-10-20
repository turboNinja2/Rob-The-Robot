



import sys


sys.path.append("/home/ulysse/python_tools")



from myparser import parser, print_in_file





second_file = "levenshtein.lol"
first_file = 'jaccard.lol'


output_file = "merged.lol"



spam_reader1 = parser(first_file, delimiter=",")
spam_reader2 = parser(second_file, delimiter=",")



spam_reader1.next()
spam_reader2.next()

results = []

for row1 in spam_reader1:
  row2 = spam_reader2.next()

  print row1
  answers = row1[1].split(" ")
  answers2 = row2[1].split(" ")

  dic_letter = {"A":0,"B":0,"C":0,"D":0}

  if len(answers) == 1:
    results.append([row1[0],answers[0]])
  else:
    for letter in answers:
      dic_letter[letter] += 2
    for letter in answers2:
      dic_letter[letter] += 1
    letter = max(dic_letter.keys(),key=lambda x : dic_letter[x]) 
    results.append([row1[0],letter])




print_in_file(results, output_file)
