// Método utilizado para Modais de alerta do site inteiro
function AbreModal(frase) {
    $('#myModal .modal-body').text(frase);
    $('#myModal').modal('show');
}