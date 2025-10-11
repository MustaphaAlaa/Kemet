import { MdOutlineAddCircleOutline } from "react-icons/md"
import Button from "../../Components/ReuseableComponents/Button"
import { NavLink } from "react-router-dom"
import { NavigationLinks } from "../../Navigations/NavigationLinks"
import type { TableConfig } from "../../Components/ReuseableComponents/Table"
import GetData from "../../APICalls/GetData"
import ApiLinks from "../../APICalls/ApiLinks"
import Table from "../../Components/ReuseableComponents/Table"

interface UserDto {
  email: string,
  userName: string,
  phoneNumber: string,
}

function UsersList() {

  const { data } = GetData<UserDto[]>(ApiLinks.auth.employees);




  const config: TableConfig[] = [

    {
      label: 'اسم المستخدم',
      render: (user: UserDto) => <div className="p-5 mx-auto rounded-full w-[1rem]">{user.userName}</div>
    },
    {
      label: 'البريد الإلكتروني',
      render: (user: UserDto) => <div className="p-3 font-bold text-sky-900"  >{user.email}</div>
    },
    {
      label: 'رقم الموبايل',
      render: (user: UserDto) => <div className="p-3 "  >{user.phoneNumber}</div>
    },
  ]

  const keyFn = (userDto: UserDto) => userDto.email

  return (
    <div className="flex flex-col mx-auto w-[96%]">
      <NavLink className="" to={NavigationLinks.users.management.create}>
        <Button className="mx-auto mt-8 flex flex-row justify-between space-x-5" primary outline hover roundedLg>
          <span>اضف موظف</span>
          <MdOutlineAddCircleOutline className="text-xl ml-1" />
        </Button>
      </NavLink>
      <Table data={data} config={config} keyFn={keyFn}  >

      </Table>
    </div>

  )
}

export default UsersList
