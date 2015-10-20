
from Globals import *






class Question():
  

  def __init__(self,row):
    self.id_position = ID_POSITION
    self.question_position  = Q_POSITION
    self.answer_position    = A_POSITION
    self.answer1_position   = A1_POSITION
    self.answer2_position   = A2_POSITION
    self.answer3_position   = A3_POSITION
    self.answer4_position   = A4_POSITION
    self.self_from_row(row) 
    


  def self_from_row(self,row):
    self.question_id  = row[self.id_position]
    self.answer       = row[self.answer_position]
    self.answer1      = row[self.answer1_position]
    self.answer2      = row[self.answer2_position]
    self.answer3      = row[self.answer3_position]
    self.answer4      = row[self.answer4_position]
