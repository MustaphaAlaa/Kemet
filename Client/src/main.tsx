import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import './styles.css'
import { RouterProvider } from 'react-router-dom'
import { router } from './routes/route'
import { Provider } from 'react-redux'
import { store } from '../store/store'
createRoot(document.getElementById('root')!).render(
  <StrictMode>
    <Provider store={store}>
      <RouterProvider router={router}></RouterProvider>
    </Provider>
  </StrictMode>,
)
