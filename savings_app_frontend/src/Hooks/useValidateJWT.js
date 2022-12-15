
const useValidateJWT = () => {

    //check user has JWT token
    return getRole(localStorage.getItem("token"));
}

function getRole(token) {

    if (token != null)
        return JSON.parse(window.atob(token.split('.')[1]))["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"]
    else
        return null;
}


export default useValidateJWT;