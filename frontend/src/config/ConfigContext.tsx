import { createContext, useContext } from 'react';
import { loadConfig, type AppConfig } from './env';

export const ConfigContext = createContext<AppConfig>(loadConfig());
export const useConfig = () => useContext(ConfigContext);
