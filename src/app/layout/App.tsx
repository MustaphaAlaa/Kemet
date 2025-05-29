// import { useState, type JSX } from 'react';
// import CustomerList from '../Components/Features/CusomersList';
import { useState } from 'react';
import CustomerForm from '../Features/CustomerForm';
import ColorManagement from '../Features/Colors/ColorManagement';
import { ColorProvider } from '../../Contexts/colors';


export type sisi = { name?: string, phoneNumber?: string, color?: string, size?: string }
function App() {

    


  return <>
    {/* <div className='container mx-auto px-4 '> */}
    <div >

      {/* <CustomerList></CustomerList> */}
      {/* <CustomerForm  ></CustomerForm> */}
      <ColorProvider>
        <ColorManagement></ColorManagement>
      </ColorProvider>

    </div>
  </>


}

export default App
