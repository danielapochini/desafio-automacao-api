//importacao da biblioteca moment do nodejs
const moment = require('moment')

// gerar data aleatoria entre a data atual e 30 dias 
function dataAleatoriaEmTrintaDias() {
    var dataAtual = new Date().getTime();
    var trintaDiasAtras = new Date(new Date().setDate(new Date().getDate() - 30));
    return moment(new Date(dataAtual + Math.random() * (trintaDiasAtras - dataAtual))).format('YYYY-MM-DD HH:mm:ss');
}
//exibe o valor da função
console.log(dataAleatoriaEmTrintaDias());
 