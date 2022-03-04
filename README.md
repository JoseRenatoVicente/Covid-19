# Covid-19

#### Projeto desenvolvido para um covidario utilizando os conceitos de fila
Seu principal objetivo é fazer o processo de um covidario desda recepção até a alta do paciente considerando apenas casos de covid e suspeita de covid a qual o programa
definira pelos dados da triagem.

## Regras de negócio
O paciente deve ir para quarentena se tiver menos de 3 dias para de sintomas do covid
Se a saturação for menor de 90 o paciente deve ser internado
Caso exista 1 sintoma da covid e tenha feito mais de 3 dias de sintomas o paciente pode fazer o exame de covid
O médico também pode avaliar e definir no sistema a situação do paciente que não se encaixe nas opções acima

## Diagrama UML feito antes do desenvolvimento para explicação do projeto

![Diagrama Atividade Covid](https://user-images.githubusercontent.com/61014145/156760101-6e35b6e1-1039-4170-9832-5792c6660be0.png)


## Funcionalidades:

- Menu com informações sobre os leitos
- Cadastro inicial paciente com seus dados básicos
- Fila priorizada para idosos, chamando 2 preferenciais 1 da fila sem preferência
- Definição se o paciente ficara de quarentena ou internado a partir da triagem
- Registro de todos os pacientes passados pelo covidario em um arquivo no formato CSV
- Fila de internação caso os números de leitos estejam indisponíveis
- Dar alta para paciente internado
- Validação das entradas do usuário


## Funcionalidades a serem implementadas
- Criptografia dos arquivos CSV
- Organização da fila de internação caso os leitos estejam indisponíveis por quantidade de comorbidades automático pelo programa

## Recurso utilizado da linguagem

- Utilização de Enums 
- Organização em pastas
- Conceitos da orientação a objetos
- Validação utilizando TryParse
- Divisão em classe das mensagens exibidas para o usuário
