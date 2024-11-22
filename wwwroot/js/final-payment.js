<script>
    $(document).ready(function (){
        $('tr').each(function () {
            var hoursWored = parseFloat($(this).find('td:eq(2)').text());
            var hourlyRate = parseFloat($(this).find('td:eq(3)').text());

            if (!isNaN(hoursWored) && !isNaN(hourlyRate)) {
                var finalPayment = (hourlyRate * hourlyRate).toFixed(2);
                $(this).find('.final-payment').text(finalPayment);
            }
        })

    })
</script>

