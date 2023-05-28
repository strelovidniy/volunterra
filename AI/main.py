import openai
import os

from flask import Flask, render_template
from routes.chat_gpt_bp import chat_gpt_bp
from routes.image_generation_bp import image_generation_bp

openai.api_key = os.getenv('OPENAI_API_KEY')

app = Flask(__name__)

app.register_blueprint(chat_gpt_bp, url_prefix='/chat_gpt')

@app.route('/')
def index():
    return '<h1>Hello World</h1>'

if __name__ == '__main__':
    app.debug = True
    app.run()