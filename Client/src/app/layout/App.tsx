import Navbar from './Navbar';
import { Outlet } from 'react-router-dom';
import { useSelector } from 'react-redux';
import Login from '../../Features/Auth/Forms/Login';

function App() {
  const auth = useSelector(state => state.auth);


  return auth.token ? <>
    <div className='grid grid-rows-[auto_1fr_auto] gap-1 h-screen' >

      <Navbar className=""></Navbar>

      <div className=''>

        <Outlet></Outlet>
      </div>


      <div className='    bg-sky-300'>
        Lorem ipsum dolor sit amet consectetur adipisicing elit. Animi dolore ea amet iste, cupiditate eaque, dolores natus, ipsam soluta nemo consectetur cum? Ab maiores officiis eius quae cupiditate dolorum tempora.
      </div>

    </div>

  </> : <Login></Login>


}

export default App
