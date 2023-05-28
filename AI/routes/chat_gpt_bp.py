from flask import Blueprint

from controllers.ChatGptController import *

chat_gpt_bp = Blueprint('chat_gpt_bp', __name__)

chat_gpt_bp.route('/', methods=['GET'])(index)
chat_gpt_bp.route('/generate_description', methods=['GET', 'POST'])(generate_description)