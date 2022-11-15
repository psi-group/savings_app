

const useValidateJWT = () => {


    //check user has JWT token
    return localStorage.getItem("token") ?  true : false


    
}




export default useValidateJWT;