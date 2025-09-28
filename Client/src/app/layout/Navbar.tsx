import { NavLink } from "react-router-dom";
import { NavigationLinks } from "../../Navigations/NavigationLinks";
import { useSelector } from "react-redux";
import { selectUserRoles } from "../../../store/store";
import { rolesTypes } from '../Auth/roles'
export default function Navbar({ className }) {
  const roles = useSelector(selectUserRoles);


  const liHover = `hover:border-l 
              hover:bg-gradient-to-r hover:from-cyan-100 hover:via-teal-100 hover:to-gray-200
              hover:-translate-x-1 ease-in-out duration-300 transition-transform
              hover:shadow-lg/30
              hover:text-sky-800
             hover:font-semibold`;


  const liMobileStyle = `border-b border-b-2 p-3 shadow-xs/10`;
  const liMdStyle = `md:border-0   md:p-2 md:shadow-none`;
  const liStyle = `  text-sm  cursor-pointer p-3 ${liHover}   ${liMdStyle} `;


  const routesBasedOnRole = {
    [rolesTypes.ADMIN]: [
      { to: "/ManageCustomers", label: "إدارة العملاء" },
      { to: "/m/Colors", label: "إدارة  الالوان" },
      { to: "/m/Sizes", label: "إدارة المقاسات" },
      { to: NavigationLinks.users.management.list, label: "إدارة المستخدمين" },
      { to: NavigationLinks.deliveryManagement.manageDelivery, label: "إدارة الشحن" }, // returns should be extract from it,

    ],
    [rolesTypes.EMPLOYEE]: [
      { to: "/ProductsPage", label: "جميع المنتجات" },

    ],
    [rolesTypes.SharedRole]: [
      { to: "/createOrder", label: "اطلب الان" },
    ]
  }


  const links2 = [];

  for (const [role, navs] of Object.entries(routesBasedOnRole)) {
    // if (roles?.includes(role) || role == rolesTypes.SharedRole) {
    for (const nav of navs) {
      links2.push(
        <NavLink className={({ isActive }) => isActive ? ' font-bold text-rose-500 p-2 border-b-1  border-red-500 ' : liStyle} key={nav.to} to={nav.to}>
          {nav.label}
        </NavLink>

      );
    }
    // }
  }


  return (
    <nav className={`${className}  p-1 text-indigo-800  `}>
      <div className="flex flex-row flex-wrap gap-1">{links2}</div>
    </nav>
  );
}
