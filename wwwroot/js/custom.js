let index = 0;

function AddTag() {
    // Get reference to TagEntry input element
    var tagEntry = document.getElementById("TagEntry");

    // Use search function to detect error state
    let searchResult = search(tagEntry.value);

    if (searchResult != null) {
        // Trigger Sweet Alert for corresponding condition
        swalWithDarkButton.fire({
            html: `<span class='font-weight-bolder'>${searchResult.toUpperCase()}</span>`
        });
    }
    else {
        // Create new Select Option
        let newOption = new Option(tagEntry.value, tagEntry.value);
        document.getElementById("TagList").options[index++] = newOption;
    }

    // Clear TagEntry control
    tagEntry.value = "";
    return true;
}

function DeleteTag() {
    let tagCount = 1;
    let tagList = document.getElementById("TagList")
    if (!tagList) return false;

    if (tagList.selectedIndex == -1) {
        swalWithDarkButton.fire({
            html: "<span class='font-weight-bolder'>SELECT A TAG BEFORE DELETING</span>"
        });
        return true;
    }

    while (tagCount > 0) {
        if (tagList.selectedIndex >= 0) {
            tagList.options[tagList.selectedIndex] = null;
            --tagCount;
        }
        else
            tagCount = 0;
        index--;
    }
}

$("form").on("submit", function () {
    $("#TagList option").prop("selected", "selected");
})

// Look for tagValues variable for data
if (tagValues != '') {
    let tagArray = tagValues.split(",");

    for (let loop = 0; loop < tagArray.length; loop++) {
        // Load or replace options we have
        ReplaceTag(tagArray[loop], loop);
        index++;
    }
}

function ReplaceTag(tag, index) {
    let newOption = new Option(tag, tag);
    document.getElementById("TagList").options[index] = newOption;
}

// Search function will detect either an empty or duplicate tag
// and return an error string if error is detected
function search(str) {
    if (str == "") {
        return 'Empty tags are not permitted';
    }

    var tagsEl = document.getElementById('TagList');

    if (tagsEl) {
        let options = tagsEl.options;
        for (let i = 0; i < options.length; i++) {
            if (options[i].value == str)
                return `The Tag #${str} was detected as a duplicate and not allowed`
        }
    }

}

const swalWithDarkButton = Swal.mixin({
    customClass: {
        confirmButton: 'btn btn-danger btn-sm btn-outline-dark d-grid'
    },
    icon: 'error',
    timer: 5000,
    buttonsStyling: false
});