import { NavLink } from "react-router-dom";

export default function Navbar({ className }) {
  const liHover = `hover:border-l hover:bg-gradient-to-r hover:from-cyan-100 hover:via-teal-100 hover:to-gray-200 hover:-translate-x-1 ease-in-out duration-300 transition-transform hover:shadow-lg/30 hover:text-sky-800`;
  const liMobileStyle = `border-b border-b-2 p-3 shadow-xs/10`;
  const liMdStyle = `md:border-0   md:p-2 md:shadow-none`;
  const liStyle = `    cursor-pointer p-3 ${liHover}   ${liMdStyle} `;

  const liObj = [
    { to: "/ManageCustomers", label: "إدارة العملاء" },
    { to: "/createOrder", label: "اطلب الان" },
    { to: "/m/Colors", label: "إدارة  الالوان" },
    { to: "/m/Sizes", label: "إدارة المقاسات" },
    { to: "/ProductsPage", label: "جميع المنتجات" },
  ];
 
  const links = liObj.map(({ to, label }) => {
    // return <li className={liStyle} key={to}>
    // return
    {
      /* <Link to={item.to}>{item.label}</Link> */
    }
    return (
      <NavLink className={({isActive})=> isActive ? 'font-bold text-rose-500 p-2   border-b border-b-1  border-red-500    ' : liStyle} key={to} to={to}>
        {label}
      </NavLink>
    );
  });

  // return <nav className={`${className}  bg-indigo-100 p-1 text-xl border-l border-l-2 border-white h-full shadow-md/30`} style={{ height: '100vh' }}>
  // return <nav className={`${className}  bg-indigo-100 p-1 text-xl border-l border-l-2 border-white w-full shadow-md/30`} style={{ height: '100vh' }}>
  return (
    <nav className={`${className}    p-1   text-indigo-800`}>
      <div className="flex flex-row flex-wrap gap-1">{links}</div>
    </nav>
  );
}
