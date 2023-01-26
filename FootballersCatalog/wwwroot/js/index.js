showInPopup = (url, title) => {
    $.ajax({
        type: 'GET',
        url: url,
        success: function (res) {
            $('#form-modal .modal-body').html(res);
            $('#form-modal .modal-title').html(title);
            $('#form-modal').modal('show');
        }
    })
}

$(() => {
    let connection = new signalR.HubConnectionBuilder().withUrl("/FootballersHub").build();

    connection.start();

    connection.on("LoadFootballers", function () {
        loadData();
    });

    loadData();

    function loadData() {
        var tr = '';

        $.ajax({
            url: '/Footballer/GetFootballers',
            method: 'GET',
            success: (result) => {
                var baseUrl = window.location.origin;
                $.each(result, (k, v) => {
                    tr = tr + `<tr>
                        <td>${v.id}</td>
                        <td>${v.firstName}</td>
                        <td>${v.lastName}</td>
                        <td>${v.gender}</td>
                        <td>${v.birthdate}</td>
                        <td>${v.team.name}</td>
                        <td>${v.country.name}</td>
                        <td>
                            <a href="#" onclick="showInPopup('${baseUrl}/Footballer/Edit/${v.id}', 'Edit Footballer')">
                                Edit
                            </a>
                        </td>
                    </tr>`;
                });

                $("#tableBody").html(tr);
            },
            error: (error) => {
                console.log(error);
            }
        });
    }
});