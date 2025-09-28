import { useEffect, useRef, useState } from 'react'
import { MdEmail, MdLogin } from 'react-icons/md';
import { useNavigate } from 'react-router-dom';
import Button from '../../../Components/ReuseableComponents/Button';
import axios from 'axios';
import ApiLinks from '../../../APICalls/ApiLinks';
import type { registerDto } from '../../../app/Models/Users/registerDto';
import { FaPhoneSquareAlt } from 'react-icons/fa';
import { NavigationLinks } from '../../../Navigations/NavigationLinks';


const Name_REGX = /^[A-Za-z][A-z0-9]/
const RegPhone = /^\d{1,11}$/;

function CreateEmployee() {
    const navigate = useNavigate();

    const userRef = useRef<HTMLInputElement>();
    const errRef = useRef<HTMLInputElement>();

    const [userName, setUsername] = useState('');
    const [userNameFocus, setUserNameFocus] = useState(false);

    const [firstName, setFirstName] = useState('');
    // const [validFirstName, setValidFirstName] = useState(false);
    const [firstNameFocus, setFirstNameFocus] = useState(false);

    const [secondName, setSecondName] = useState('');
    // const [validLastName, setValidLastName] = useState(false);
    const [lastNameFocus, setLastNameFocus] = useState(false);

    const [phoneNumber, setPhoneNumber] = useState('');
    const [validPhoneNubmer, setValidPhoneNumber] = useState(false);

    const [email, setEmail] = useState('');
    const [emailFocus, setEmailFocus] = useState(false);

    const [password, setPassword] = useState('');
    const [passwordFocus, setPasswordFocus] = useState(false);

    const [confirmPassword, setMatchPassword] = useState('');
    const [validMatchPassword, setValidMatchPassword] = useState(false);
    const [matchFocus, setMatchFocus] = useState(false);

    const [errMsg, setErrMsg] = useState<string[] | string>('');
    const [success, setSuccess] = useState(false);




    useEffect(() => {
        userRef.current.focus()
        console.log(userRef.current)
    }, [])


    useEffect(() => {
        setErrMsg('')
    }, [userName, password])

    useEffect(() => {
        setValidMatchPassword(password == confirmPassword)
    }, [password, confirmPassword])

    const register: registerDto = {
        email,
        userName,
        firstName,
        secondName,
        phoneNumber,
        password,
        confirmPassword
    }

    const handleSubmit = async (e) => {
        e.preventDefault()

        try {

            const data = await axios.post(ApiLinks.auth.createEmployee, register)
                .catch();
            navigate(NavigationLinks.users.management.list)
        } catch (err) {

            setErrMsg(err?.response.data?.errorMessages[0].split(", "));
            errRef.current.focus();
        }
    }

    const handlePhoneNumberInput = (e) => {
        const val = e.target.value;
        console.log(val)
        if (val.length == 0) setPhoneNumber('');

        if (RegPhone.test(val)) {
            console.log(val)
            setPhoneNumber(val)
            setValidPhoneNumber(true);
        }
        else setValidPhoneNumber(false);

    };

    const formGroup = 'flex flex-col   justify-between space-y-2'
    const inputStyle = 'shadow-lg/30 border-1  border-gray-300 p-2 text-center  bg-white rounded-lg ';


    return <section className="    flex flex-col  my-15">

        <div className='mx-auto   space-y-2 w-[95%] flex flex-col  rounded-xl shadow-xl/50 bg-white border-3 border-cyan-100'>
            <h1 className='self-center text-xl font-bold m-2'> إنشاء موظف جديد</h1>
            <p ref={errRef} className={errMsg ? "bg-rose-300 text-rose-800 font-bold" : "offscreen"} aria-live="assertive">{errMsg}</p>

            <form onSubmit={handleSubmit} className='bg-gradient-to-tl from-emerald-50 via-teal-100 to-cyan-200 flex flex-col p-8 m-1 rounded-xl space-y-9 font-bold'  >
                <div className='flex flex-col lg:flex-row  justify-center lg:justify-between items-center space-y-5 lg:space-y-0 '>
                    <div className={formGroup + ` w-[100%]  lg:w-[45%]`} >
                        <label className=' self-center flex flex-row items-center' htmlFor="firstname">
                            الاسم الاول
                        </label>
                        <input
                            dir='ltr'
                            className={inputStyle}
                            type="text"
                            id="firstname"
                            ref={userRef}
                            value={firstName}
                            onChange={(e) => setFirstName(e.target.value)}
                            onFocus={() => setFirstNameFocus(true)}
                            onBlur={() => setFirstNameFocus(false)}
                            required
                        />
                    </div >
                    <div className={formGroup + ` w-[100%] lg:w-[45%]`} >
                        <label className='self-center flex flex-row items-center' htmlFor="lastname">
                            الاسم الثانى
                        </label>
                        <input
                            dir='ltr'
                            className={inputStyle}
                            type="text"
                            id="lastname"
                            value={secondName}
                            onChange={(e) => setSecondName(e.target.value)}
                            onFocus={() => setLastNameFocus(true)}
                            onBlur={() => setLastNameFocus(false)}
                            required
                        />
                    </div>
                </div>
                <div className={formGroup} >
                    <label className='font-bold self-center flex flex-row items-center' htmlFor="username">
                        اسم المستخدم
                    </label>
                    <input
                        dir='ltr'
                        className={inputStyle}
                        type="text"
                        id="username"
                        onChange={(e) => setUsername(e.target.value)}
                        onFocus={() => setUserNameFocus(true)}
                        onBlur={() => setUserNameFocus(false)}
                        required
                    />
                </div>
                <div className={formGroup} >
                    <label className=' self-center flex flex-row items-center' htmlFor="phoneNumber">
                        <span>
                            رقم الموبايل
                        </span>
                        <FaPhoneSquareAlt />
                    </label>
                    <input
                        dir='ltr'
                        className={inputStyle}
                        type="text"
                        id="phoneNumber"
                        value={phoneNumber}
                        onChange={handlePhoneNumberInput}
                        required
                    />
                    {!validPhoneNubmer && phoneNumber.length != 0 && <p className='text-center font-bold bg-rose-200 text-rose-600 p-1 rounded-md shadow-md/20'>
                        رقم الموبايل لازم يكون 11 رقم
                        وبالشكل ده
                        01XXXXXXXXX
                    </p>}
                </div>
                <div className={formGroup} >
                    <label className=' self-center flex flex-row items-center' htmlFor="email">
                        <span>
                            البريد الإلكترونى
                        </span>
                        <MdEmail />
                    </label>
                    <input
                        dir='ltr'
                        className={inputStyle}
                        type="email"
                        id="email"
                        value={email}
                        onChange={(e) => setEmail(e.target.value)}
                        onFocus={() => setEmailFocus(true)}
                        onBlur={() => setEmailFocus(false)}
                        required
                    />
                </div>


                <div className={formGroup}>
                    <label className='text-center' htmlFor="password">كلمة المرور </label>
                    <input
                        dir='ltr'
                        className={inputStyle}
                        type="password"
                        id="password"
                        onChange={(e) => setPassword(e.target.value)}
                        value={password}
                        onFocus={() => setPasswordFocus(true)}
                        onBlur={() => setPasswordFocus(false)}
                        required
                    />
                </div>

                <div className={formGroup}>
                    <label className='text-center' htmlFor="confirm_password">تأكيد كلمة المرور </label>
                    <input
                        dir='ltr'
                        className={inputStyle}
                        type="password"
                        id="confirm_password"
                        onChange={(e) => setMatchPassword(e.target.value)}
                        value={confirmPassword}
                        onFocus={() => setMatchFocus(true)}
                        onBlur={() => setMatchFocus(false)}
                        required
                    />
                    {!validMatchPassword && <p className='text-center font-bold bg-rose-200 text-rose-600 p-1'>
                        The Password Doens't Match
                    </p>}
                </div>

                <Button success hover roundedFull styles="w-[50%] md:w-[30%] flex flex-row items-center justify-between mx-auto text-center font-bold " >
                    <span>
                        إنشاء حساب جديد
                    </span>
                    <MdLogin className='text-2xl font-bold' />

                </Button>
            </form>
        </div>


    </section>

}

export default CreateEmployee
