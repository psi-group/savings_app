const useIsRestaurant = () => {

    return getRole(localStorage.getItem("token")) == "seller" ? true : getRole(localStorage.getItem("token")) == "buyer" ? false : null;
}

export default useIsRestaurant;


function getRole(token) {

    if (token != null)
        return JSON.parse(window.atob(token.split('.')[1]))["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"]
    else
        return null;
}