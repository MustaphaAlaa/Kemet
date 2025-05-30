import CustomerList from '../../Components/Features/CusomersList';
import { useState } from 'react';
import CustomerForm from '../../Features/CustomerForm';
import ColorManagement from '../../Features/Colors/ColorManagement';
import { ColorProvider } from '../../Contexts/colors';
import Sidebar from './Sideabar';
import { NavigationProvider } from '../../Contexts/navigation';
import Route from '../../Components/ReuseableComponents/Route';


 function App() {




  return <>
    {/* <div className='container mx-auto px-4 '> */}
    <div className='flex flex-col md:flex-row justify-between' >
      <NavigationProvider>
        <Sidebar className=""></Sidebar>

        <Route path='/'>
          <CustomerForm></CustomerForm>
        </Route>

        <Route path='/m/Colors'>
          <ColorProvider>
            <ColorManagement></ColorManagement>
          </ColorProvider>
        </Route>
      
      </NavigationProvider >
    </div>

  </>


}

export default App
