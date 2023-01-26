showNewTeamInput = (that) => {
    if (that.value == "0") {
        $('#newTeam').css("display", "block");
        $('#newTeam input').prop("disabled", false);
    } else {
        $('#newTeam').css("display", "none");
        $('#newTeam input').prop("disabled", true);
    }
}