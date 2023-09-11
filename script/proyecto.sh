#!/bin/bash

echo "Escriba uno de los siguientes comandos para que sea ejecutado"
echo " "
echo "run :Para ejecutar el proyecto"
echo "report : compilar el informe y generar su pdf"
echo "slides : Compilar la presentacion y generar su pdf"
echo "show_report : Para abrir el informe del proyecto en un visualizador"
echo "show_slides : Para abrir la presentacion del proyecto en un visualizador"
echo "clean : Para borrar los archivos basura generados por la compilacion"
echo " "
echo "Comando: "

read comando

function report {
cd ../informe
pdflatex "informe.tex"
}
function slides {
cd ../presentacion
pdflatex "presentacion.tex"
}
function clean {
GLOBIGNORE=informe.pdf:informe.tex:presentacion.pdf:presentacion.tex:imagenes
    cd ../informe
    rm -v *
    cd ../presentacion
    rm -v *
}
function show_report {
cd ../informe
if [ ! -f "informe.pdf" ]; then
	report
fi
echo "Escriba un visualizador entre ( linux-gnu o darwin ) o presione enter para usar uno por defecto"
read visualizador
if   [[ "$OSTYPE" == "linux-gnu"* ]]; then
	xdg-open "informe.pdf"
elif [[ "$OSTYPE" == "darwin"* ]]; then
	open "informe.pdf"
else
	start "informe.pdf"
fi
}
function show_slides {
cd ../presentacion
if [ ! -f "presentacion.pdf" ]; then
	slides
fi
echo "Escriba un visualizador entre ( linux-gnu o darwin ) o presione enter para usar uno por defecto"
read visualizador
if   [[ "$OSTYPE" == "linux-gnu"* ]]; then
	xdg-open "presentacion.pdf"
elif [[ "$OSTYPE" == "darwin"* ]]; then
	open "presentacion.pdf"
else
	start "presentacion.pdf"
fi
}
function run {
cd ".."
make dev
}

case "$comando" in
run)
	run
;;
report)
	report
;;
slides)
	slides
;;
show_report)
	show_report
;;
show_slides)
	show_slides
;;
clean)
	clean
;;
*)
	echo "Opción inválida"
esac
