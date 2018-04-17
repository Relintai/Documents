@echo off

cd Headers

pdflatex Article1d3A4Page.tex
copy Article1d3A4Page.pdf ..\Build\DiMat_1_Article_Scaled1d3A4.pdf

pdflatex Article1d3A4PageVariant.tex
copy Article1d3A4Page.pdf ..\Build\DiMat_1_Article_Scaled1d3A4_Variant.pdf

pdflatex Article1d4A4Page.tex
copy Article1d4A4Page.pdf ..\Build\DiMat_1_Article_Scaled1d4A4.pdf

pdflatex Article1d5A4Page.tex
copy Article1d5A4Page.pdf ..\Build\DiMat_1_Article_Scaled1d5A4.pdf

pdflatex Article1d6A4Page.tex
copy Article1d6A4Page.pdf ..\Build\DiMat_1_Article_Scaled1d6A4.pdf

pdflatex ArticleA4Page.tex
copy ArticleA4Page.pdf ..\Build\DiMat_1_Article_Normal.pdf

pdflatex ArticleHalfA4Page.tex
copy ArticleHalfA4Page.pdf ..\Build\DiMat_1_Article_HalfA4.pdf

pdflatex ArticleHalfA4Page2x1.tex
copy ArticleHalfA4Page2x1.pdf ..\Build\DiMat_1_Article_HalfA4_2x1.pdf

rem pdflatex ArticleA4Page2x2.tex
rem copy ArticleA4Page2x2.pdf ..\Build\DiMat_1_Article_A4Page_2x2.pdf

rem pdflatex ArticleHalfA4Page4x2.tex
rem copy ArticleHalfA4Page4x2.pdf ..\Build\DiMat_1_Article_HalfA4_4x2.pdf

pdflatex PrezA4Page.tex
copy PrezA4Page.pdf ..\Build\DiMat_1_Prez_Normal.pdf

rem pdflatex Article1d2A5Page.tex
rem copy Article1d2A5Page.pdf ..\Build\DiMat_1_Article_Scaled1d2A5.pdf

pause