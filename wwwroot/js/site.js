$(document).ready(function() {
    $(".has-token-kat").select2({
        allowClear:true,
        placeholder: 'Wpisz swoje kategorie tutaj'
      })
});
$(document).ready(function() {
    $(".has-token-skl").select2({
        tags:true,
        allowClear:true,
        placeholder: 'Wpisz swoje składniki tutaj'
      })
});