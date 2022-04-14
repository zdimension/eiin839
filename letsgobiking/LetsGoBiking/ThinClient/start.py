from http.server import HTTPServer, SimpleHTTPRequestHandler

PORT = 8000

httpd = HTTPServer(('', PORT), SimpleHTTPRequestHandler)
print("Serving on port", PORT)

import webbrowser
webbrowser.open(f"http://localhost:{PORT}/")

httpd.serve_forever()
