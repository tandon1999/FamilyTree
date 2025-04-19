window.onUpdateProfile = (user) => {
    let name = user?.profile?.name ?? "N/A";
    let el = document.getElementById("username");
    if (el) {
        el.innerText = name;
    } else {
        console.warn("Element with ID 'username' not found.");
    }
};
