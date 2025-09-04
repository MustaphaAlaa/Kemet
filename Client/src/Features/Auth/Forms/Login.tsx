import { useRef, useState, useEffect } from 'react'
import { useNavigate } from 'react-router-dom'

import { useDispatch } from 'react-redux'
import { setCredentials, useLoginMutation } from '../../../../store/store'
import { MdEmail, MdLogin } from 'react-icons/md'
import Button from '../../../Components/ReuseableComponents/Button'

const Login = () => {
    const userRef = useRef<HTMLInputElement>()
    const errRef = useRef<HTMLInputElement>()
    const [user, setUser] = useState('')
    const [pwd, setPwd] = useState('')
    const [errMsg, setErrMsg] = useState('')
    const navigate = useNavigate()

    const [login, { isLoading }] = useLoginMutation()
    const dispatch = useDispatch()

    useEffect(() => {
        userRef.current.focus()
        console.log(userRef.current)
    }, [])

    useEffect(() => {
        setErrMsg('')
    }, [user, pwd])

    const handleSubmit = async (e) => {
        e.preventDefault()

        try {
            const userData = await login({ user, pwd }).unwrap()
            dispatch(setCredentials({ ...userData, user }))
            setUser('')
            setPwd('')
            navigate('/welcome')
        } catch (err) {
            // if (!err?.originalStatus) {
            //     // isLoading: true until timeout occurs
            //     setErrMsg('No Server Response');
            // } else if (err.originalStatus === 400) {
            //     setErrMsg('Missing Username or Password');
            // } else if (err.originalStatus === 401) {
            //     setErrMsg('Unauthorized');
            // } else {
            //     setErrMsg('Login Failed');
            // }
            console.log(err);
            errRef.current.focus();
        }
    }

    const handleUserInput = (e) => setUser(e.target.value)

    const handlePwdInput = (e) => setPwd(e.target.value)

    const formGroup = 'flex flex-col   justify-between space-y-2'
    const inputStyle = 'shadow-lg/30 border-1  border-gray-300 p-2    text-center';


    const content = isLoading ? <h1>Loading...</h1> : (
        <section className="  h-screen flex flex-col   ">
            <p ref={errRef} className={errMsg ? "bg-rose-300 text-rose-800 font-bold" : "offscreen"} aria-live="assertive">{errMsg}</p>

            <div className='mx-auto mt-20 space-y-2 w-[80%] flex flex-col bg-sky-300 rounded-xl shadow-xl/50'>
                <h1 className='self-center text-xl font-bold m-2'>Employee Login</h1>

                <form onSubmit={handleSubmit} className='bg-white flex flex-col p-8 m-1 rounded-xl space-y-9'  >
                    <div className={formGroup} >
                        <label className=' self-center flex flex-row items-center' htmlFor="username">
                            <MdEmail />
                            <span>
                                البريد الإلكترونى
                            </span>
                        </label>
                        <input
                            dir='ltr'
                            className={inputStyle}
                            type="text"
                            id="username"
                            ref={userRef}
                            value={user}
                            onChange={handleUserInput}
                            autoComplete="off"
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
                            onChange={handlePwdInput}
                            value={pwd}
                            required
                        />
                    </div>

                    <Button success hover roundedFull styles="w-[50%] md:w-[30%] flex flex-row items-center justify-between mx-auto text-center font-bold " >
                        <span>
                            تسجيل الدخول
                        </span>
                        <MdLogin className='text-2xl font-bold' />

                    </Button>
                </form>
            </div>


        </section>
    )

    return content
}
export default Login