@echo off

SET inputfile="Document"
SET outputprefix="Java"

if not exist ".\Build" mkdir .\Build

cd Build

pdflatex "\newcommand\includemaindocument{\input{../%inputfile%.tex}}\input{../../Headers/Article1d3A4Page.tex}"
copy Article1d3A4Page.pdf ..\Build\%outputprefix%_1_Article_Scaled1d3A4.pdf

pdflatex "\newcommand\includemaindocument{\input{../%inputfile%.tex}}\input{../../Headers/Article1d3A4PageVariant.tex}"
copy Article1d3A4PageVariant.pdf ..\Build\%outputprefix%_1_Article_Scaled1d3A4_Variant.pdf

pdflatex "\newcommand\includemaindocument{\input{../%inputfile%.tex}}\input{../../Headers/Article1d4A4Page.tex}"
copy Article1d4A4Page.pdf ..\Build\%outputprefix%_1_Article_Scaled1d4A4.pdf

pdflatex "\newcommand\includemaindocument{\input{../%inputfile%.tex}}\input{../../Headers/Article1d5A4Page.tex}"
copy Article1d5A4Page.pdf ..\Build\%outputprefix%_1_Article_Scaled1d5A4.pdf

pdflatex "\newcommand\includemaindocument{\input{../%inputfile%.tex}}\input{../../Headers/Article1d6A4Page.tex}"
copy Article1d6A4Page.pdf ..\Build\%outputprefix%_1_Article_Scaled1d6A4.pdf

pdflatex "\newcommand\includemaindocument{\input{../%inputfile%.tex}}\input{../../Headers/ArticleA4Page.tex}"
copy ArticleA4Page.pdf ..\Build\%outputprefix%_1_Article_Normal.pdf

pdflatex "\newcommand\includemaindocument{\input{../%inputfile%.tex}}\input{../../Headers/ArticleHalfA4Page.tex}"
copy ArticleHalfA4Page.pdf ..\Build\%outputprefix%_1_Article_HalfA4.pdf

pdflatex "\newcommand\includemaindocument{\input{../%inputfile%.tex}}\input{../../Headers/ArticleHalfA4Page2x1.tex}"
copy ArticleHalfA4Page2x1.pdf ..\Build\%outputprefix%_1_Article_HalfA4_2x1.pdf

pdflatex "\newcommand\includemaindocument{\input{../%inputfile%.tex}}\input{../../Headers/ArticleA4Page2x2.tex}"
copy ArticleA4Page2x2.pdf ..\Build\%outputprefix%_1_Article_A4Page_2x2.pdf

pdflatex "\newcommand\includemaindocument{\input{../%inputfile%.tex}}\input{../../Headers/ArticleHalfA4Page4x2.tex}"
copy ArticleHalfA4Page4x2.pdf ..\Build\%outputprefix%_1_Article_HalfA4_4x2.pdf

pdflatex "\newcommand\includemaindocument{\input{../%inputfile%.tex}}\input{../../Headers/PrezA4Page.tex}"
copy PrezA4Page.pdf ..\Build\%outputprefix%_1_Prez_Normal.pdf

pause