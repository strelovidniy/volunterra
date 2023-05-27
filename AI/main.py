import os
import openai
from flask import Flask

openai.api_key = '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'

completion = openai.ChatCompletion.create(
  model="gpt-3.5-turbo",
  temperature=0.7,
  messages=[
    {"role": "user", "content": "Generate the description to the volunteering post about opened fundraising for armor"}
  ]
)

app = Flask(__name__)

@app.route('/')
def hello_world():
    return completion.choices[0].message.content

@app.route('/generate_description')
def generate_description():
    return completion.choices[0].message.content
