
## ML.Net Language Detector Sample

### Este es un simple ejemplo de aprendizaje de maquina hecho en ML.NET framework.

Este proyecto ofrece un modelo ML.NET que detecta mediante una entrada de texto en qué idioma se encuentra dicho texto.
El projecto contiene pruebas unitarias, un ejemplo con consola y projecto web hecho con Blazor Server. La idea es
en principio muy sencilla, en este ejemplo se usa un algoritmo de multi clasificacion para indicarle al modelo,
como asociar cierta orden de palabras y la escritura de palabras mismas a un lenguaje. 
El algoritmo que se decidio usar es 'NaiveBayes' que entre otras cosas, ofrece para el set de datos usado
un impresiionate performance y precision. 
El conjuntod e datos para entrenamientos no es ideal del todo, pero se consiguio al final muy buenos resultados
el empleamiento de multi clasificacion. Lo ideal es pasarle al algoritmo una carga de datos mucho mas notable y bien
orgnanziado en funcion de supervisar con aun mayor exactitud el entrenamiento.
