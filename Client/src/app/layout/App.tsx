import Navbar from './Navbar';
import { Outlet, useLocation } from 'react-router-dom';
import { useSelector } from 'react-redux';
import Login from '../../Features/Auth/Forms/Login';

import {
  QueryClient,
  QueryClientProvider,
} from '@tanstack/react-query'
import { NavigationLinks } from '../../Navigations/NavigationLinks';
import { ProductOrderPage } from '../../routes/elementsToRouting';

const queryClient = new QueryClient();

function App() {
  const auth = useSelector(state => state.auth);
  const url = useLocation();

  const content = auth.token ? <>
    <div className='grid grid-rows-[auto_1fr_auto] gap-1 h-screen' >

      <Navbar className=""></Navbar>

      <div className=''>

        <Outlet></Outlet>
      </div>


      <div className='    bg-sky-300'>
        Lorem ipsum dolor sit amet consectetur adipisicing elit. Animi dolore ea amet iste, cupiditate eaque, dolores natus, ipsam soluta nemo consectetur cum? Ab maiores officiis eius quae cupiditate dolorum tempora.
      </div>

    </div>

  </> : url.pathname.includes(NavigationLinks.product.orderProductPage) ? <ProductOrderPage></ProductOrderPage> : <Login></Login>;


  return <QueryClientProvider client={queryClient}>
    {content}
  </QueryClientProvider>




}

export default App
