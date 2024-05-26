from ID_Data_Extraction_helper import *
from flask import Flask, request, Blueprint
import json
from deepface import DeepFace

Models = ["VGG-Face", "Facenet", "Facenet512", 
          "OpenFace", "DeepFace", "DeepID", "ArcFace",
          "Dlib", "SFace"]

app = Flask(__name__)
micro_services = Blueprint('micro_services', __name__, url_prefix='/micro_services')

@micro_services.after_request
def add_header(response):
    response.headers['Content-Type'] = 'application/json'
    return response

@micro_services.route('/face_recognition', methods=['POST'])
def face_recognition():
    try:
        id_card_image = preprocess_image(request.files.get("id_card_image"))
        camera_image =  preprocess_image(request.files.get("camera_image"))
        result = DeepFace.verify(id_card_image, camera_image, model_name = Models[4], detector_backend="mtcnn", 
                                 distance_metric="cosine", enforce_detection=True, align=True, normalization="base")
        response_data = {'result': "Face verification successful." if result["verified"] else "Face verification failed."}
        return json.dumps(response_data), 200
    except Exception as e:
        message = {'error': 'Failed to process the images', 'details': str(e)}
        return json.dumps(message), 500
@micro_services.route('/data_extraction', methods=['POST'])
def data_extraction():
    try:
        id_card_image = preprocess_image(request.files.get("id_card_image"))
        # id_card_image = cv2.imread("id_card_image-.jpg")
        gray,edged,image_contour,image_Trasform,image_Tgraym,dst= scan_ID(id_card_image)
        response_data= extract_Text(gray if image_Tgraym is None else image_Tgraym)

        return json.dumps(response_data), 200
    except Exception as e:
        return json.dumps({'error': 'Failed to process the image', 'details': str(e)}), 500
app.register_blueprint(micro_services)
if __name__ == '__main__':
    app.run(port=5000)