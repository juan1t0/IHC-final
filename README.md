# IHC-Shooting Mike's

Este es un juego de realidad virtual, similar a una simulación del tiro al plato. Esta influenciado por la cultura popular, como peliculas (Monsters INC), comics (X-men), y otros videojuegos (Duck Hunt).
## Requerimientos:
- Cámara kinect, para reconocer los gestos de los jugadores y trackear sus movimientos.

- Unity 2019.2.1 o superior. Los Assets que permiten (1) captar la información desde la Kinect. Texturas y efectos de sonido (proporcionados en este mismo repositorio). (2) utilizar el Google Cardboard como dispositivo de salida.

- Google CardBoard, utilizado como el visor principal para los jugadores
## Funcionamiento:
* Jugador 1: Para lanzar el disco, el jugador debe poner su brazo izquierdo por encima de sus hombros, el lanzamiento es efectuado cuando el jugador extiende su brazo.

* Jugador 2: Para disparar, utiliza el pulsador disponible con la CardBoard, la dirección se obtiene por el puntero, tambien proporcionado por la CardBoard.

## Ejecución:
Solo se tiene que iniciar dos proyecto en unity desde la carpeta donde sea descargado este repositorio.
El primero tiene las configuraciones necesarias para ser el servidor del juego. Desde la segunda carpeta se podrá cargar el proyecto en unity para los jugadores.
Las diferencias recaen en el protocolo de comunicación y los eventos que algunos mensajes provocan.
