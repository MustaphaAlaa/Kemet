import Sidebar from './Sidebar';
import { Outlet } from 'react-router-dom';


function App() {




  return <>
    {/* <div className='container mx-auto px-4 '> */}
    <div className='flex flex-col  justify-between' >

      <Sidebar className=""></Sidebar>

      <Outlet></Outlet>
    </div>

  </>

}

export default App
