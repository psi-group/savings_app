import { Outlet } from 'react-router-dom';

const Layout = () => {
    return (

        <>
            <Header></Header>
            <main className="App">
                <Outlet />
            </main>
        </>
        

        )
}