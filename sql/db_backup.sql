--
-- PostgreSQL database dump
--

-- Dumped from database version 13.4
-- Dumped by pg_dump version 15.3

-- Started on 2023-08-28 10:30:54

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'WIN1251';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

CREATE DATABASE "WebApplicationAssesment" WITH TEMPLATE = template0 ENCODING = 'UTF8';


ALTER DATABASE "WebApplicationAssesment" OWNER TO postgres;

\connect "WebApplicationAssesment"

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'WIN1251';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

--
-- TOC entry 4 (class 2615 OID 2200)
-- Name: public; Type: SCHEMA; Schema: -; Owner: postgres
--

-- *not* creating schema, since initdb creates it


ALTER SCHEMA public OWNER TO postgres;

--
-- TOC entry 217 (class 1255 OID 3436047)
-- Name: sp_database_seed(); Type: PROCEDURE; Schema: public; Owner: postgres
--

CREATE PROCEDURE public.sp_database_seed()
    LANGUAGE plpgsql
    AS $$
begin
    -- Delete records
	delete from public."user" where 1=1;
	delete from public.account where 1=1;
	delete from public."role" where 1=1;
	
    -- Role Seeder
    insert into public."role" (id, name, created_at) values (1, 'Administrator', '2023-08-28') ;

    -- Account Seeder
   	insert into public.account(id, username, password, salt, role_id, created_at) values (1, 'jp_1992@yopmail.com', '70FBBC0635B570D15BDBD33F0F8991430C3889E91D7AD6E417D7810A8B012A3C7F610B0FBA91F19B639906A8016D336A097CD78E77C0C898BB124F10A41F0853', 'D12B99B0BA7DBB12E2E4AE23A31998F9BBBCD0DF92F3DA5D07419FADEB30DE0713675F7F28C9424C70189A73DACC15D4E7AC683BFA9FE023C4BD42EF432463E5', 1, '2023-08-28');
	
	-- User Seeder
	
	insert into public."user"(id, firstname, lastname, account_id, created_at) values (1, 'Juan', 'Pinzon', 1, '2023-08-28');
    commit;
end;$$;


ALTER PROCEDURE public.sp_database_seed() OWNER TO postgres;

SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- TOC entry 201 (class 1259 OID 3435912)
-- Name: account; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.account (
    id bigint NOT NULL,
    username character varying NOT NULL,
    password character varying NOT NULL,
    created_at timestamp without time zone NOT NULL,
    role_id bigint NOT NULL,
    is_deleted boolean DEFAULT false NOT NULL,
    updated_at timestamp without time zone,
    deleted_at timestamp without time zone,
    salt character varying
);


ALTER TABLE public.account OWNER TO postgres;

--
-- TOC entry 203 (class 1259 OID 3435970)
-- Name: account_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.account_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.account_seq OWNER TO postgres;

--
-- TOC entry 3025 (class 0 OID 0)
-- Dependencies: 203
-- Name: account_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.account_seq OWNED BY public.account.id;


--
-- TOC entry 204 (class 1259 OID 3435976)
-- Name: role_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.role_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.role_seq OWNER TO postgres;

--
-- TOC entry 202 (class 1259 OID 3435955)
-- Name: role; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.role (
    id bigint DEFAULT nextval('public.role_seq'::regclass) NOT NULL,
    name character varying NOT NULL,
    created_at timestamp without time zone NOT NULL,
    is_deleted boolean DEFAULT false NOT NULL,
    updated_at timestamp without time zone,
    deleted_at timestamp without time zone
);


ALTER TABLE public.role OWNER TO postgres;

--
-- TOC entry 205 (class 1259 OID 3435980)
-- Name: user_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.user_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.user_seq OWNER TO postgres;

--
-- TOC entry 200 (class 1259 OID 3435873)
-- Name: user; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."user" (
    id bigint DEFAULT nextval('public.user_seq'::regclass) NOT NULL,
    firstname character varying NOT NULL,
    lastname character varying NOT NULL,
    created_at timestamp without time zone NOT NULL,
    is_deleted boolean DEFAULT false NOT NULL,
    updated_at timestamp without time zone,
    deleted_at timestamp without time zone,
    account_id bigint NOT NULL
);


ALTER TABLE public."user" OWNER TO postgres;

--
-- TOC entry 2868 (class 2604 OID 3435983)
-- Name: account id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.account ALTER COLUMN id SET DEFAULT nextval('public.account_seq'::regclass);


--
-- TOC entry 3013 (class 0 OID 3435912)
-- Dependencies: 201
-- Data for Name: account; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.account (id, username, password, created_at, role_id, is_deleted, updated_at, deleted_at, salt) FROM stdin;
1	jp_1992@yopmail.com	70FBBC0635B570D15BDBD33F0F8991430C3889E91D7AD6E417D7810A8B012A3C7F610B0FBA91F19B639906A8016D336A097CD78E77C0C898BB124F10A41F0853	2023-08-28 00:00:00	1	f	\N	\N	D12B99B0BA7DBB12E2E4AE23A31998F9BBBCD0DF92F3DA5D07419FADEB30DE0713675F7F28C9424C70189A73DACC15D4E7AC683BFA9FE023C4BD42EF432463E5
\.


--
-- TOC entry 3014 (class 0 OID 3435955)
-- Dependencies: 202
-- Data for Name: role; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.role (id, name, created_at, is_deleted, updated_at, deleted_at) FROM stdin;
1	Administrator	2023-08-28 00:00:00	f	\N	\N
\.


--
-- TOC entry 3012 (class 0 OID 3435873)
-- Dependencies: 200
-- Data for Name: user; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."user" (id, firstname, lastname, created_at, is_deleted, updated_at, deleted_at, account_id) FROM stdin;
1	Juan	Pinzon	2023-08-28 00:00:00	f	\N	\N	1
\.


--
-- TOC entry 3026 (class 0 OID 0)
-- Dependencies: 203
-- Name: account_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.account_seq', 1, true);


--
-- TOC entry 3027 (class 0 OID 0)
-- Dependencies: 204
-- Name: role_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.role_seq', 1, true);


--
-- TOC entry 3028 (class 0 OID 0)
-- Dependencies: 205
-- Name: user_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.user_seq', 1, true);


--
-- TOC entry 2875 (class 2606 OID 3435919)
-- Name: account Account_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.account
    ADD CONSTRAINT "Account_pkey" PRIMARY KEY (id);


--
-- TOC entry 2879 (class 2606 OID 3435962)
-- Name: role Role_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.role
    ADD CONSTRAINT "Role_pkey" PRIMARY KEY (id);


--
-- TOC entry 2873 (class 2606 OID 3435880)
-- Name: user User_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."user"
    ADD CONSTRAINT "User_pkey" PRIMARY KEY (id);


--
-- TOC entry 2877 (class 2606 OID 3436014)
-- Name: account unique_username_constraint; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.account
    ADD CONSTRAINT unique_username_constraint UNIQUE (username) INCLUDE (username);


--
-- TOC entry 2881 (class 2606 OID 3435963)
-- Name: account account_role_fk; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.account
    ADD CONSTRAINT account_role_fk FOREIGN KEY (role_id) REFERENCES public.role(id) ON UPDATE SET NULL ON DELETE SET NULL NOT VALID;


--
-- TOC entry 2880 (class 2606 OID 3436028)
-- Name: user user_account_fk; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."user"
    ADD CONSTRAINT user_account_fk FOREIGN KEY (account_id) REFERENCES public.account(id) ON UPDATE CASCADE ON DELETE CASCADE NOT VALID;


--
-- TOC entry 3024 (class 0 OID 0)
-- Dependencies: 4
-- Name: SCHEMA public; Type: ACL; Schema: -; Owner: postgres
--

REVOKE USAGE ON SCHEMA public FROM PUBLIC;
GRANT ALL ON SCHEMA public TO PUBLIC;


-- Completed on 2023-08-28 10:30:54

--
-- PostgreSQL database dump complete
--

