const Unauthorized = () => {


    return (
        <>
            <h1>You are not authorized to access this page</h1>
            <button className="mt-3 bg-sky-500 text-white rounded-xl font-bold hover:bg-sky-800 hover:text-white h-10 w-full"
            >    
               Log in
            </button>
        </>
    )

}

export default Unauthorized;