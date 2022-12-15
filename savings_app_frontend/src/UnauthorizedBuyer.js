const UnauthorizedBuyer = () => {


    return (
        <>
            <h1>Only buyers can access this page!</h1>
            <button className="mt-3 bg-sky-500 text-white rounded-xl font-bold hover:bg-sky-800 hover:text-white h-10 w-full"
            >
                Home
            </button>
        </>
    )

}

export default UnauthorizedBuyer;