import { NavLink } from "react-router-dom";

export default function Sidebar({ className }) {



    const liHover = `hover:border-l hover:bg-gradient-to-r hover:from-cyan-100 hover:via-teal-100 hover:to-gray-200 hover:-translate-x-1 ease-in-out duration-300 transition-transform hover:shadow-lg/30 hover:text-sky-800`;
    const liMobileStyle = `border-b border-b-2 p-3 shadow-xs/10`;
    const liMdStyle = `md:border-0   md:p-2 md:shadow-none`;
    const liStyle = `font-bold text-white cursor-pointer p-3 ${liHover}   ${liMdStyle} `;



    const liObj = [
        { to: '/createOrder', label: 'إدارة العملاء' },
        { to: '/m/Colors', label: 'التحكم فى الالوان' },
    ]


    const links = liObj.map(({ to, label }) => {
        return <li className={liStyle} key={to}>
            {/* <Link to={item.to}>{item.label}</Link> */}
            <NavLink to={to}>{label}</NavLink>
        </li>
    });

    // return <nav className={`${className}  bg-indigo-100 p-1 text-xl border-l border-l-2 border-white h-full shadow-md/30`} style={{ height: '100vh' }}>
    // return <nav className={`${className}  bg-indigo-100 p-1 text-xl border-l border-l-2 border-white w-full shadow-md/30`} style={{ height: '100vh' }}>
    return <nav className={`${className}  bg-indigo-500 p-1  shadow-md/30 text-white`}>
        <ul className="flex flex-row flex-wrap gap-1">
            {links}
        </ul>

    </nav>
}