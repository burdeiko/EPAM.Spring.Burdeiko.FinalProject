var onModalLoad = function (result) {
    $('#ChangeAvatar').html(result).dialog();
}

var onSubmitSuccess = function (result) {
    if (!result.success) {
        $('#ChangeAvatar').html(result);
    } else {
        alert('thanks for submitting');
        $('#ChangeAvatar').dialog('close');
    }
};