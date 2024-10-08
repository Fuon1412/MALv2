import './assets/main.css'
import 'ant-design-vue/dist/reset.css';
import Antd from 'ant-design-vue';
import { createApp } from 'vue'
import App from './App.vue'
import { Form, Input, Button, Checkbox } from "ant-design-vue";
import router from './route';

const app = createApp(App).mount('#app');

app.use(Antd);
app.use(Form);
app.use(Input);
app.use(Button);
app.use(Checkbox);
app.use(router);
