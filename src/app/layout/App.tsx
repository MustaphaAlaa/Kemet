import ProductVariantPricePage from '../../Features/Product/ProductManagment/Price/ProductPricePage';
import Navbar from './Navbar';
import { Outlet } from 'react-router-dom';


function App() {




  return <>
    {/* <div className='container mx-auto px-4 '> */}
    <div className='flex flex-col  justify-between' >

      <Navbar className=""></Navbar>

        {/* <ProductVariantPricePage></ProductVariantPricePage> */}
      <Outlet></Outlet>

    
    
    </div>

  </>

}

export default App
