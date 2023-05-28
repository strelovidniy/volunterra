import openai

response = openai.Completion.create(
  model="text-davinci-003",
  prompt="I am a highly intelligent question answering bot. If you ask me a question that is rooted in truth, I will give you the answer. Q: What is earth",
  temperature=0,
  max_tokens=100,
  top_p=1,
  frequency_penalty=0.0,
  presence_penalty=0.0,
  stop=["\n"]
)

completion = openai.ChatCompletion.create(
  model="gpt-3.5-turbo",
  temperature=0.7,
  messages=[
    {"role": "user", "content": "Уяви, що я пишу допис у соціальні мережі для пошуку волонтерів. Допоможи мені з написанням опису для допомоги плетіння маскувальних сіток. Опис повинен бути мінімум на 10-15 речень"}
  ]
)

hello_completion = openai.ChatCompletion.create(
  model="gpt-3.5-turbo",
  temperature=0.7,
  messages=[
    {"role": "user", "content": "Привітайся з усіма волонтерами!"}
  ]
)

def index():
    return hello_completion.choices[0].message.content

def generate_description():
    return completion.choices[0].message.content