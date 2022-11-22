
function getId(token) {
    console.log(JSON.parse(window.atob(token.split(".")[1])));
    return JSON.parse(window.atob(token.split(".")[1]))[
        "Id"
    ];
}

function getName(token) {
    console.log(JSON.parse(window.atob(token.split(".")[1])));
    return JSON.parse(window.atob(token.split(".")[1]))[
        "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"
    ];
}

const Profile = () => {


    return (
        <>
            <div className="text-xl text-center  mt-5 text-3xl font-bold "> Your Profile</div>
           
            <div className=" float-left  ml-[80px] w-[275px] h-[325px]  mt-[50px] border-solid border-2 border-sky-500">
                    <div className="d-flex flex-column align-items-center text-center mt-4">
                        <img src="https://bootdey.com/img/Content/avatar/avatar7.png" alt="Admin" className="rounded-circle" width="150"/>
                        <div className="mt-3">
                            <h4>{getName(localStorage.getItem("token"))}</h4>
                            <p className="text-secondary mb-1">Country: Lithuania</p>
                            <p className="text-muted font-size-sm">City: Vilnius</p> 
                        </div>
                </div>
            </div  >

            <div className="float-left w-[600px] h-[325px] mt-[50px] ml-20 border-solid border-2 border-sky-500">
                <div className="card-body  w-[500px] h-[300px] text-xm ml-3">
                    <div className="row  mt-3 mb-3 border-solid w-[500px] h-[30px]  border-2">
                        <div className="col-sm-5">
                        <h6 className="mb-0">Name:</h6>
                    </div>
                    <div className="col-sm-5 text-secondary"> Admin</div>
                </div>
                    <div className="row  mt-3 mb-3 border-solid w-[500px] h-[30px]  border-2">
                        <div className="col-sm-5">
                                    <h6 className="mb-0">Email:</h6>
                                </div>
                                <div className="col-sm-5 text-secondary"> fip@jukmuh.al</div>
                            </div>
                                <div>
                        <div className="row  mt-3 mb-3 border-solid w-[500px] h-[30px]  border-2">
                            <div className="col-sm-5 mt-[3.5px] ">
                                            <h6 className="">Phone:</h6>
                                            </div>
                                            <div className="col-sm-5 text-secondary"> (239) 816-9029</div>
                                            </div>
                                            <div>
                            <div className="row  mt-3 mb-3 border-solid w-[500px] h-[30px]  border-2">
                                <div className="col-sm-5 mt-[3.5px]">
                                        <h6 className="mb-0">Orders count:</h6>
                                    </div>
                                    <div className="col-sm-5 text-secondary"> 999999999</div>
                                    </div>
                            <div>
                                <div className="row  mt-3 mb-3 border-solid w-[500px] h-[30px]  border-2">
                                    <div className="col-sm-3 mt-[3.5px]">
                                        <h6 className="mb-0">Address:</h6>
                                    </div>
                                    <div className="col-sm-9 text-secondary"> Bay Area, San Francisco </div>
                                            </div>
                                  <div>
                                    <div className="row">
                                        <div className="col-sm-3">
                                        <button className="btn btn-info ">Edit</button>
                                        </div> 
                                        </div> 
                                        </div> 
                                        </div> 
                                        </div>
                                     </div>
                                        </div>
                </div>
        </>
      
    )
}

export default Profile;