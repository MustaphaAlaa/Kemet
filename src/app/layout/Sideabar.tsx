import Link from "../../Components/ReuseableComponents/Link";
import Route from "../../Components/ReuseableComponents/Route";
import ColorManagement from "../../Features/Colors/ColorManagement";
import CustomerForm from "../../Features/CustomerForm";

export default function Sidebar({ className }) {



    const liHover = `hover:border-l hover:bg-gradient-to-r hover:from-cyan-100 hover:via-teal-100 hover:to-gray-200 hover:-translate-x-1 ease-in-out duration-300 transition-transform hover:shadow-lg/30 hover:text-sky-800`;
    const liMobileStyle = `border-b border-b-2 p-3 shadow-xs/10`;
    const liMdStyle = `md:border-0   md:p-2 md:shadow-none`;
    const liStyle = `font-bold text-blue-500 cursor-pointer ${liHover} ${liMobileStyle} ${liMdStyle} `;



    const liObj = [
        { to: '/', label: 'إدارة العملاء' },
        { to: '/m/Colors', label: 'التحكم فى الالوان' },
    ]


    const links = liObj.map(item => {
        return <li className={liStyle} key={item.to}>
            <Link to={item.to}>{item.label}</Link>
        </li>
    });

    return <nav className={`${className}  bg-white p-1 text-xl border-l border-gray-300 h-full shadow-md/30`} style={{ height: '100vh' }}>
        <ul className="mt-3 flex flex-row md:flex-col flex-wrap gap-1">
            {links}
        </ul>
               <Route path='/'>
          <CustomerForm></CustomerForm>
        </Route>
        <Route path='/m/colors'>
          <ColorManagement></ColorManagement>
        </Route>

    </nav>
}